import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../../assets/json/config';


@Injectable({
    providedIn: 'root'
})
export class ConfigService {
    public config!: AppConfig['config'];

    constructor(private http: HttpClient) { }

    async load() {
        return new Promise<void>((res, rej) => {
            this.http
            .get<AppConfig>('./assets/json/config.json')
            .subscribe(config => {
                localStorage.setItem('config', JSON.stringify(config['config']));
                this.config = config['config'];
                res();
            }, (error) => { rej(`Failed to load app configuration. ${error}`) });
        })
    }

    get api() { return this.config.api; }
}
