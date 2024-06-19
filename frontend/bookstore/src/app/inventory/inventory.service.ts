import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Inventory } from './inventory';
import { InventorySummary } from './inventory-summary';
import { ConfigService } from '../services/config.service';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  private readonly baseUrl: string;

  constructor(private httpClient: HttpClient, configService: ConfigService) {
    this.baseUrl = configService.api.inventoryApi;
  }

  getSummary() {
    return this.httpClient.get<InventorySummary[]>(this.baseUrl + '/summary');
  }

  registerResource(id: string) {
    return this.httpClient.post<Inventory>(this.baseUrl + '/register', { resourceId: id });
  }
}
