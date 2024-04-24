import { Component, EventEmitter, Output } from '@angular/core';
import { routeItems } from '../layouts/admin-layout/admin-layout.routes';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  title: string = 'Dashboard';
  @Output() onToggle = new EventEmitter<void>();

  constructor(private readonly router: Router) {
    this.router.events.pipe(filter(evt => evt instanceof NavigationEnd)).subscribe((_) => {
      const activeRoute = routeItems.find(item => this.router.isActive(item.path, false));
      this.title = activeRoute?.title ?? 'Dashboard';
    });
  }
}
