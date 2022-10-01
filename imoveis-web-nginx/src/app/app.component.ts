import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { HotToastService } from '@ngneat/hot-toast';
import { BehaviorSubject, catchError, map, Observable, of } from 'rxjs';
import { CreateFormComponent } from './components/create-form/create-form.component';

export interface ImovelResponse {
  id: number;
  endereco: string;
  tipo: string;
  tipoContrato: string;
  valor: number;
}

export interface ImovelRequest {}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'imoveis-web';
  imoveis$$!: BehaviorSubject<ImovelResponse[]>;
  imoveis$!: Observable<ImovelResponse[]>;

  constructor(
    readonly httpClient: HttpClient,
    readonly matDialog: MatDialog,
    private readonly toast: HotToastService
  ) {
    this.getImoveis();
  }

  getImoveis() {
    this.httpClient
      .get<ImovelResponse[]>('https://teste-denis.herokuapp.com/v1/imoveis')
      .pipe(
        catchError((err) => {
          this.toast.error('Erro ao buscar imóveis!');
          return of([]);
        }),
        map((imoveis) => {
          return imoveis.sort((a, b) => {
            return a.id - b.id;
          });
        })
      )
      .subscribe((imoveis) => {
        if (!this.imoveis$$) {
          this.imoveis$$ = new BehaviorSubject(imoveis);
          this.imoveis$ = this.imoveis$$.asObservable();
          return;
        }
        this.imoveis$$.next(imoveis);
      });
  }

  openDialog() {
    this.matDialog
      .open(CreateFormComponent, {
        width: '500px',
        height: '500px',
      })
      .afterClosed()
      .subscribe((imovel: ImovelRequest) => {
        this.getImoveis();
      });
  }
  openDialogEdit(imovel: ImovelRequest) {
    this.matDialog
      .open(CreateFormComponent, {
        width: '500px',
        height: '500px',
        data: imovel,
      })
      .afterClosed()
      .subscribe((imovel: ImovelRequest) => {
        this.getImoveis();
      });
  }

  remove(id: number) {
    this.httpClient
      .delete(`https://teste-denis.herokuapp.com/v1/imoveis/${id}`)
      .pipe(
        catchError((err) => {
          this.toast.success('Imóvel removido com sucesso!');
          this.getImoveis();

          throw err;
        })
      )
      .subscribe();
  }
}
