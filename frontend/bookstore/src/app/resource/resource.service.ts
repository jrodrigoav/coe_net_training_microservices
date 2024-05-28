import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Resource } from './resource';
import { map } from 'rxjs/operators';
import { ConfigService } from '../services/config.service';

@Injectable({
  providedIn: 'root'
})
export class ResourceService {
  private readonly baseUrl: string;

  constructor(private httpClient: HttpClient, configService: ConfigService) {
    this.baseUrl = configService.api.resourceApi;
  }

  getList() {
    return this.httpClient.get<Resource[]>(this.baseUrl+"/list");
  }

  create(resource: Omit<Resource, 'id'>) {
    return this.httpClient.post<Resource>(this.baseUrl, resource);
  }

  update(id: string, resource: Omit<Resource, 'id'>) {
    return this.httpClient.put<Resource>(this.baseUrl + id, { id, ...resource });
  }

  delete(id: string) {
    return this.httpClient.delete(this.baseUrl + id, { observe: 'response' })
      .pipe(map(response => response.status >= 200 && response.status <= 299));
  }
}
