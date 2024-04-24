import { Component } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule, Validators, FormArray } from '@angular/forms';
import { ResourceService } from './resource.service';
import { AsyncPipe, DatePipe, NgFor } from '@angular/common';
import { Observable } from 'rxjs';
import { Resource } from './resource';
import { InventoryService } from '../inventory/inventory.service';

@Component({
  selector: 'app-resource',
  standalone: true,
  imports: [NgFor, AsyncPipe, DatePipe, ReactiveFormsModule],
  providers: [DatePipe],
  templateUrl: './resource.component.html',
  styleUrl: './resource.component.css'
})
export class ResourceComponent {
  tableColumns = ['ID', 'Name', 'Author', 'Type', 'Date of Publication'];
  public resources: Observable<Resource[]>;
  public searchString?: string;
  public formGroup: FormGroup;
  public titleModal: any;
  public id?: string;
  get tags(): FormArray {
    return this.formGroup.controls['tags'] as FormArray;
  };

  constructor(private resourceService: ResourceService, private inventoryService: InventoryService, private datePipe: DatePipe) {
    this.formGroup = new FormGroup({
      name: new FormControl('', [Validators.required]),
      dateOfPublication: new FormControl<Date>(new Date(), [Validators.required]),
      author: new FormControl('', [Validators.required]),
      tags: new FormArray([]),
      type: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required])
    });
    this.resources = this.resourceService.getList();
    this.titleModal = "New Resource";
  }

  onTagInput(evt: KeyboardEvent) {
    if(evt.key === ',' || evt.key === 'Enter') {
      const input = (evt.target as HTMLInputElement);
      const value = input.value;
      if(value) {
        this.tags.push(new FormControl(value));
      }
      input.value = '';
      evt.preventDefault();
    }
  }

  removeTag(index: number) {
    this.tags.removeAt(index);
  }

  submitModal() {
    const resource = this.formGroup.value;
    if (this.id) {
      this.resourceService.update(this.id, resource).subscribe((_) => {
        this.resources = this.resourceService.getList();
      }, (error) => {
        console.error(error)
      });
    }
    else {
      this.resourceService.create(resource).subscribe((_) => {
        this.resources = this.resourceService.getList();
      });
    }
  }

  deleteResource() {
    if(!this.id) return;
    this.resourceService.delete(this.id).subscribe(() => {
      // TODO: Handle error and success message
      this.resources = this.resourceService.getList();
    });
  }

  register(id: string) {
    this.inventoryService.registerResource(id).subscribe(result => {
      // TODO: Handle error and success message
      console.log(result);
    });
  }

  onNew() {
    this.titleModal = "New Resource";
    this.id = undefined;
    this.formGroup.reset();
  }

  onUpdate(row: any) {
    this.titleModal = "Update Resource";
    this.id = row.id;
    this.tags.controls = [];
    this.formGroup.reset();
    this.tags.controls = row.tags.map((tag: string) => new FormControl(tag));
    this.formGroup.patchValue({ ...row, dateOfPublication: this.datePipe.transform(new Date(row.dateOfPublication), 'yyyy-MM-dd', '+0000')});
  }

  onDelete(id: any) {
    this.id = id;
  }
}
