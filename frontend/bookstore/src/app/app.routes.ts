import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadChildren: () => import('./layouts/admin-layout/admin-layout.routes').then(m => m.routes)
    },
    { path: '**', redirectTo: '' },
];
