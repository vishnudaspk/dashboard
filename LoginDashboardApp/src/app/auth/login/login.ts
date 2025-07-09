import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth'; // Adjusted path
import { CommonModule } from '@angular/common'; // For *ngIf
import { FormsModule } from '@angular/forms'; // For ngModel, ngForm

@Component({
  selector: 'app-login',
  templateUrl: './login.html', // Adjusted path
  styleUrls: ['./login.css'], // Adjusted path
  // Make dependencies explicit for the template
  // Even if part of AppModule, this ensures component has direct access
  // This is more aligned with standalone component philosophy
  imports: [CommonModule, FormsModule],
  standalone: true // Required if 'imports' is used directly in @Component
})
export class LoginComponent {
  loginData = {
    username: '',
    password: ''
  };
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) { }

  onLogin(): void {
    this.errorMessage = null;
    if (!this.loginData.username || !this.loginData.password) {
        this.errorMessage = "Username and password are required.";
        return;
    }
    this.authService.login(this.loginData).subscribe({
      next: () => {
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        console.error('Login failed:', err);
        if (err.status === 401) {
            this.errorMessage = "Invalid username or password.";
        } else if (err.status === 429) {
            this.errorMessage = "Too many login attempts. Please try again later.";
        }
        else {
            this.errorMessage = "Login failed. Please try again later.";
        }
      }
    });
  }
}
