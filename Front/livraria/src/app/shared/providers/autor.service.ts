import { Autor } from './../models/autor';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class AutorService {

  private API_URL: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  public inserir(autor: Autor): Observable<Autor>  {
    return this.httpClient.post<Autor>(`${this.API_URL}autor`, autor);
  }

  public listar(): Observable<Array<Autor>> {
    return this.httpClient.get<Array<Autor>>(`${this.API_URL}autor`);
  }

  public obterPorId(autorId: number): Observable<Autor> {
    return this.httpClient.get<Autor>(`${this.API_URL}autor/${autorId}`);
  }

  public alterar(autor: Autor,autorId: number): Observable<Autor> {
    return this.httpClient.put<Autor>(`${this.API_URL}autor/${autorId}`, autor);
  }

  public excluir(autorId: number): Observable<Autor> {
    return this.httpClient.delete<Autor>(`${this.API_URL}autor/${autorId}`);
  }
}
