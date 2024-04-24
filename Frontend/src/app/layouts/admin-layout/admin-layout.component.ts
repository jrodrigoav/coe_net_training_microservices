import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '../../sidebar/sidebar.component';
import { NavbarComponent } from '../../navbar/navbar.component';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-admin-layout',
  standalone: true,
  imports: [NgClass, RouterOutlet, SidebarComponent, NavbarComponent],
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.css'
})
export class AdminLayoutComponent {
  expanded: boolean = false;
}
