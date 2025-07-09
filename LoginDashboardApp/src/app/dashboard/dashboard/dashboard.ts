import { Component, OnInit } from '@angular/core';
import { ChartConfiguration, ChartType } from 'chart.js'; // ChartOptions might be implicitly covered or not strictly needed for template
import { DashboardService } from '../dashboard'; // Adjusted path
import { AuthService } from '../../auth/auth'; // Adjusted path
import { Router } from '@angular/router'; // To navigate on logout
import { CommonModule } from '@angular/common'; // For *ngIf
import { NgChartsModule } from 'ng2-charts'; // For baseChart directive - Reverting to NgChartsModule

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.html', // Adjusted path
  styleUrls: ['./dashboard.css'], // Adjusted path
  // Make dependencies explicit for the template
  imports: [CommonModule, NgChartsModule], // Reverting to NgChartsModule
  standalone: true // Required if 'imports' is used directly in @Component
})
export class DashboardComponent implements OnInit {

  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic data.
    scales: {
      x: {},
      y: {
        min: 0
      }
    },
    plugins: {
      legend: {
        display: true,
      }
    }
  };
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [];

  public barChartData: ChartConfiguration['data'] = {
    labels: [], // Populated from API
    datasets: [] // Populated from API
  };

  errorMessage: string | null = null;

  constructor(
    private dashboardService: DashboardService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadChartData();
  }

  loadChartData(): void {
    this.errorMessage = null;
    this.dashboardService.getChartData().subscribe({
      next: (data) => {
        if (data && data.labels && data.datasets) {
          this.barChartData = {
            labels: data.labels,
            datasets: data.datasets.map((ds: any) => ({
              data: ds.data,
              label: ds.label,
              backgroundColor: ds.backgroundColor,
              borderColor: ds.borderColor,
              borderWidth: ds.borderWidth
            }))
          };
        } else {
          // Fallback to some default display or error if data is not as expected
          this.barChartData = {
            labels: [ 'Error' ],
            datasets: [ { data: [0], label: 'Could not load data' } ]
          };
          this.errorMessage = "Chart data is in an unexpected format.";
        }
      },
      error: (err) => {
        console.error('Failed to load chart data:', err);
        this.errorMessage = 'Failed to load chart data. Please try again later.';
         if (err.status === 401) { // Unauthorized
            this.authService.logout(); // Token might be invalid or expired
        }
        // Display a fallback or error message in the chart area
        this.barChartData = {
            labels: [ 'Error' ],
            datasets: [ { data: [0], label: 'Failed to load data' } ]
          };
      }
    });
  }

  logout(): void {
    this.authService.logout();
  }
}
