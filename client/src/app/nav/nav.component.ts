import { Component, inject, input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AppComponent } from '../app.component';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  private router = inject(Router);
  private toastr = inject(ToastrService);
  model: any = {};

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => {;
        void this.router.navigateByUrl('/members');
      },
      error: e => this.toastr.error(e.error)
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }
}
