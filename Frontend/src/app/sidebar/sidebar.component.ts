import { NgIf, NgFor, NgClass } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { routeItems } from '../layouts/admin-layout/admin-layout.routes';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [NgIf, NgFor, NgClass, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  @Output() onToggle = new EventEmitter<void>();
  @Input() expanded: boolean = false;
  menuItems = routeItems;

  constructor(router: Router) {
    router.events.pipe(filter(evt => evt instanceof NavigationEnd))
      .subscribe(() => {
        if(window.matchMedia("(min-width: 768px)") && this.expanded) {
          this.onToggle.next();
        }
      })
  }
}
