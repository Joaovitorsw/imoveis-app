import { HttpClient } from '@angular/common/http';
import {
  ChangeDetectionStrategy,
  Component,
  Inject,
  OnInit,
} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HotToastService } from '@ngneat/hot-toast';
import { catchError } from 'rxjs';
import { ImovelRequest, ImovelResponse } from 'src/app/app.component';

@Component({
  selector: 'app-create-form',
  templateUrl: './create-form.component.html',
  styleUrls: ['./create-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateFormComponent implements OnInit {
  imovelForm = new FormGroup({
    endereco: new FormControl('', [Validators.required]),
    tipo: new FormControl('', [Validators.required]),
    tipoContrato: new FormControl('', [Validators.required]),
    valor: new FormControl('', [Validators.required]),
  });

  constructor(
    @Inject(MAT_DIALOG_DATA) readonly data: ImovelResponse,
    readonly httpClient: HttpClient,
    readonly matDialogRef: MatDialogRef<CreateFormComponent>,
    private readonly toast: HotToastService
  ) {}

  ngOnInit(): void {
    if (this.data) {
      this.imovelForm.patchValue(this.data);
    }
  }
  onSubmit() {
    if (this.imovelForm.invalid) return;

    if (this.data) {
      this.onUpdate();
      return;
    }

    this.onCreate();
  }

  onCreate() {
    const imovel: ImovelRequest = this.imovelForm.value;
    this.httpClient
      .post<ImovelRequest>(
        'https://teste-denis.herokuapp.com/v1/imoveis',
        {
          responseType: 'text',
        },
        imovel
      )
      .pipe(
        catchError((err) => {
          this.toast.success('Imóvel criado com sucesso!');
          this.matDialogRef.close();

          throw err;
        })
      )
      .subscribe();
  }

  onUpdate() {
    const imovel: ImovelRequest = this.imovelForm.value;
    this.httpClient
      .put<ImovelRequest>(
        `https://teste-denis.herokuapp.com/v1/imoveis/${this.data.id}`,
        {
          responseType: 'text',
        },
        imovel
      )
      .pipe(
        catchError((err) => {
          this.toast.success('Imóvel atualizado com sucesso!');
          this.matDialogRef.close();
          throw err;
        })
      )
      .subscribe();
  }
}
