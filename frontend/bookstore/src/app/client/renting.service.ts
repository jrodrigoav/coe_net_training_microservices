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

  constructor(private httpClient: HttpClient, configService: ConfigService) {
    this.baseUrl = configService.api.rentingApi;
  }

  listByClientId(id: string) {
    return this.httpClient.get<ClientRenting[]>(this.baseUrl + `client/${id}`);
  }

  register(data: Renting) {
    return this.httpClient.post<Omit<Renting, 'id'>>(this.baseUrl + 'register/', data);
  }

  returnResource(id: string, returnDate: Date | string) {
    return this.httpClient.put(this.baseUrl + `return/${id}`, { returnDate });
  }
}
