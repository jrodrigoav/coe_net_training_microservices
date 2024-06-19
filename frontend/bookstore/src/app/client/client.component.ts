import { Component } from '@angular/core';
import { ClientService } from './client.service';
import { debounceTime, map } from 'rxjs/operators';
import { BehaviorSubject, Observable, Subject, combineLatest, firstValueFrom } from 'rxjs';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AsyncPipe, DatePipe, NgFor } from '@angular/common';
import { Resource } from '../resource/resource';
import { ResourceService } from '../resource/resource.service';
import { Client } from './client';
import { RentingService } from './renting.service';
import { ClientRenting } from './client-renting';

@Component({
  selector: 'app-client',
  standalone: true,
  imports: [NgFor, AsyncPipe, DatePipe, FormsModule, ReactiveFormsModule],
  templateUrl: './client.component.html',
  styleUrl: './client.component.css'
})
export class ClientComponent {
  tableColumns = ['ID', 'First Name','Last Name', 'Email'];
  loadedClients: Subject<Client[]> = new Subject();
  tableRows: Observable<Client[]>;
  searchString?: string;
  search: BehaviorSubject<string> = new BehaviorSubject('');
  resources: Observable<Resource[]>;
  rentingList?: Observable<ClientRenting[]>;
  clientForm: FormGroup;
  rentingGroup: FormGroup;
  modalTitle?: string;
  id?: string;
  date?: Date;

  handleSearch(evt: Event) {
    this.search.next((evt.target as HTMLInputElement).value);
  }

  constructor(
    private readonly clientService: ClientService,
    private readonly rentingService: RentingService,
    resourceService: ResourceService
  ) {
    this.getClients();
    this.tableRows = combineLatest([
      this.search.pipe(debounceTime(300)),
      this.loadedClients,
    ]).pipe(map(([searchText, data]) => {
      if (!searchText) return data;
      return data.filter(d => d.firstName.toLowerCase().includes(searchText.toLowerCase()));//TODO we may need to add lastName as well
    }));

    this.resources = resourceService.getList();

    this.rentingGroup = new FormGroup({
      resourceId: new FormControl('', [Validators.required]),
      clientId: new FormControl('', [Validators.required]),
      registrationDate: new FormControl(new Date(Date.now()), [Validators.required])
    });

    this.clientForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required])
    });
  }

  getClients() {
    this.clientService.getList().subscribe(clients => {
      this.loadedClients.next(clients);
    });
  }

  submitClientModal() {
    const client: Client = this.clientForm.value;
    if (this.id) {
      this.clientService.update(this.id, client).subscribe(() => {
        this.getClients();
      });
    }
    else {
      this.clientService.create(client).subscribe(() => {
        this.getClients();
      });;
    }
  }

  returnResource() {
    console.error(this.id, this.date);
    if(!this.id) return;

    if(!this.date) return;

     this.rentingService.returnResource(this.id, this.date).subscribe(() => {
       // Handle update
     });
  }

  returnSpecificResource() {
    console.error(this.id, this.date);
    if(!this.id) return;

    if(!this.date) return;

     this.rentingService.returnSpecificResource(this.id, this.date).subscribe(() => {
       // Handle update
     });
  }

  deleteClient() {
    if(!this.id) return;
    this.clientService.delete(this.id);
  }

  onNew() {
    this.modalTitle = "New Client";
    this.id = undefined;
  }

  onUpdate(row: any) {
    this.modalTitle = "Update Client";
    this.id = row.id;
    this.clientForm.reset();
    this.clientForm.patchValue(row);
  }

  submitRentingModal() {
    const renting = this.rentingGroup.value;
    this.rentingService.register(renting).subscribe((_) => {
      // Handle register
    });
  }

  async onRenting(id: string) {
    const loadedResources = await firstValueFrom(this.resources);
    if(!loadedResources.length) return;
    
    this.rentingGroup.reset();
    this.rentingGroup.patchValue({
      resourceId: loadedResources[0].id,
      clientId: id
    });
  }

  onReturn(id: string) {
    this.id = id;
    this.rentingList = this.rentingService.listByClientId(id);
  }

  onReturnDate(id: string,returnDate: Date|undefined) {
    this.id = id;
    this.date = undefined;
    this.rentingService.returnSpecificResource(id,returnDate);
  }
}
