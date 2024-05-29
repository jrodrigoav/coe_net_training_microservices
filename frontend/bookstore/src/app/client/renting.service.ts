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


  constructor(private httpClient: HttpClient, configService: ConfigService) {
    this.baseUrl = configService.api.rentingApi;
    this.resourceApiUrl = configService.api.resourceApi;
  }

  listByClientId(id: string) {
    return this.httpClient.get<ClientRenting[]>(`${this.resourceApiUrl}/${id}`);//TODO check if this is correct
  }

  register(data: Renting) {
    return this.httpClient.post<Omit<Renting, 'id'>>(`${this.baseUrl}/register`, data);
  }

  returnResource(id: string, returnDate: Date | string) {
    return this.httpClient.put(this.baseUrl + `return/${id}`, { returnDate });
  }
}
