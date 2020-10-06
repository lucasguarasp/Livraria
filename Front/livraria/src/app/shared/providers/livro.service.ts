import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Livro } from '../models/livro';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  private API_URL: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  public listar(): Observable<Array<Livro>> {
    return this.httpClient.get<Array<Livro>>(`${this.API_URL}livro`);
  }

  public inserir(livro: Livro): Observable<Livro>  {
    return this.httpClient.post<Livro>(`${this.API_URL}livro`, livro);
  }

  public obterPorId(livroId: number): Observable<Livro> {
    return this.httpClient.get<Livro>(`${this.API_URL}livro/${livroId}`);
  }

  public alterar(livro: Livro, livroId: number): Observable<Livro> {
    return this.httpClient.put<Livro>(`${this.API_URL}livro/${livroId}`, livro);
  }

  public comprar(livroId: number): Observable<Livro> {
    return this.httpClient.put<Livro>(`${this.API_URL}livro/comprar/${livroId}`, null);
  }


  public excluir(livroId: number): Observable<Livro> {
    return this.httpClient.delete<Livro>(`${this.API_URL}livro/${livroId}`);
  }
}
