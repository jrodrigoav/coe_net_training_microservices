import { Injectable } from '@angular/core';
import { Renting } from './renting';
import { HttpClient } from '@angular/common/http';
import { ClientRenting } from './client-renting';
import { ConfigService } from '../services/config.service';

@Injectable({
  providedIn: 'root'
})
export class RentingService {
  private readonly baseUrl: string;
  private readonly resourceApiUrl: string;
  private readonly rentingApiUrl: string;


  constructor(private httpClient: HttpClient, configService: ConfigService) {
    this.baseUrl = configService.api.rentingApi;
    this.resourceApiUrl = configService.api.resourceApi;
    this.rentingApiUrl = configService.api.rentingApi;
  }

  listByClientId(clientId: string) {
    return this.httpClient.get<ClientRenting[]>(`${this.rentingApiUrl}/rented/${clientId}`);//TODO check if this is correct
  }

  register(data: Renting) {
    return this.httpClient.post<Omit<Renting, 'id'>>(`${this.baseUrl}/register`, data);
  }

  returnSpecificResource(resourceId: string, returnDate: Date | undefined) {
    return this.httpClient.put(`${this.rentingApiUrl}/return/${resourceId}`, { returnDate });
  }

  returnResource(resourceId: string, returnDate: Date | string) {
    return this.httpClient.put(`${this.rentingApiUrl}/returnoeuoeu/${resourceId}`, { returnDate });
  }
}
