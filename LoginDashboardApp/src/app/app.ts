import { Component } from '@angular/core';

@Component({
  selector: 'app-root', // This selector is used in src/index.html
  templateUrl: './app.html', // Links to the main app template
  styleUrls: ['./app.css'],   // Links to global app styles if any, or component-specific
  standalone: false // Explicitly mark as not standalone
})
export class AppComponent {
  title = 'LoginDashboardApp'; // A common property, can be removed if not used
}
