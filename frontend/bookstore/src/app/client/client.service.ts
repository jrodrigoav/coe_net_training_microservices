import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Client } from './client';
import { map } from 'rxjs/operators';
import { ConfigService } from '../services/config.service';

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  private readonly baseUrl: string;

  constructor(private httpClient: HttpClient, configService: ConfigService) {
    this.baseUrl = configService.api.clientApi;
  }

  getList() {
    return this.httpClient.get<Client[]>(this.baseUrl);
  }

  create(data: Omit<Client, 'id'>) {
    return this.httpClient.post<Client>(this.baseUrl, data);
  }

  update(id: string, data: Omit<Client, 'id'>) {
    return this.httpClient.put<Client>(this.baseUrl , { ...data, id });
  }

  delete(id: string) {
    return this.httpClient.delete(this.baseUrl + id, { observe: 'response' })
      .pipe(map(response => response.status >= 200 && response.status <= 299));
  }
}
