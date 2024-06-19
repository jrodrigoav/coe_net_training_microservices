import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InventoryService } from './inventory.service';
import { BehaviorSubject, Observable, combineLatest, debounceTime, map } from 'rxjs';
import { AsyncPipe, NgFor } from '@angular/common';
import { InventorySummary } from './inventory-summary';

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [NgFor, AsyncPipe, FormsModule],
  templateUrl: './inventory.component.html',
  styleUrl: './inventory.component.css'
})
export class InventoryComponent {
  tableColumns: string[] = ['ID', 'Name', 'Available Copies', 'Unavailable Copies', 'Total'];
  searchString?: string;
  data: Observable<InventorySummary[]>;
  tableRows: Observable<InventorySummary[]>;
  search: BehaviorSubject<string> = new BehaviorSubject('');

  constructor(private readonly inventoryService: InventoryService) {
    this.data = this.inventoryService.getSummary();
    this.tableRows = combineLatest([
      this.search.pipe(debounceTime(300)),
      this.data,
    ]).pipe(map(([searchText, data]) => {
      if(!searchText) return data;
      return data.filter(d => d.resourceName.toLowerCase().includes(searchText.toLowerCase()));
    }));
  }

  handleSearch(evt: Event) {
    this.search.next((evt.target as HTMLInputElement).value);
  }
}
