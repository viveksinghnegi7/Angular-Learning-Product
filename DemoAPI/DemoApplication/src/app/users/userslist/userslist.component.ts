import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Users } from "../../models/Users";
import { UserService } from "../../services/user.service";
import { MatSort } from '@angular/material/sort';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UserComponent } from '../user/user.component';

@Component({
  selector: 'app-userslist',
  templateUrl: './userslist.component.html',
  styleUrls: ['./userslist.component.css']
})
export class UserslistComponent implements OnInit {
  constructor(private userService: UserService, private dialog: MatDialog) { }
  displayedColumns: string[] = ['userId', 'email', 'firstName', 'lastName', 'actions'];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  searchKey: string;
  ngOnInit(): void {
    this.userService.getAllUsers()
      .subscribe(response => {
        console.log(response);
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.dataSource.filterPredicate = (data, filter) => {
          return this.displayedColumns.some(ele => {
            return ele != 'actions' && data[ele].toLowerCase().indexOf(filter) != -1;
          });
        };
      }); 
  }

  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }

  applyFilter() { 
    this.dataSource.filter = this.searchKey.trim().toLowerCase();
  }


  onCreate() {
    this.userService.initializeFormGroup();
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    this.dialog.open(UserComponent, dialogConfig);
  }

  onEdit(row) {
    this.userService.populateForm(row);
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    this.dialog.open(UserComponent, dialogConfig);
  }

  onDelete(userId) {
    if (confirm('Are you sure to delete this record ?')) {
      this.userService.deleteUser(userId);
      //this.notificationService.warn('! Deleted successfully');
    }
  }
}








