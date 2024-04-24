import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout.component';
import { ResourceComponent } from '../../resource/resource.component';
import { ClientComponent } from '../../client/client.component';
import { InventoryComponent } from '../../inventory/inventory.component';

export const routes: Routes = [
    {
        path: '',
        component: AdminLayoutComponent,
        children: [
            { path: '', redirectTo: 'resources', pathMatch: 'full', },
            { path: 'resources', component: ResourceComponent },
            { path: 'clients', component: ClientComponent },
            { path: 'inventory', component: InventoryComponent }
        ]
    },
];

export const routeItems = [
    { path: '/resources', title: 'Resources',  icon: 'bi bi-bar-chart' },
    { path: '/clients', title: 'Clients',  icon:'bi bi-people' },
    { path: '/inventory', title: 'Inventory',  icon:'bi bi-box' }
];