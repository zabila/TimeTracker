import {HttpClient} from '@angular/common/http';
import {Component} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public accountDtos?: AccountDto[];

  constructor(http: HttpClient) {
    http.get<AccountDto[]>('https://localhost:7058/api/accounts').subscribe(result => {
      this.accountDtos = result;
      this.accountDtos.forEach(accountDto => console.log(accountDto));
    }, error => console.error(error));
  }

  title = 'TimeTrackerApp';
}

interface AccountDto {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  type: number;
}
