import { AdresaIsporuke } from "./korisnik"

export interface DodatiProizvodi {
    idDodatiProizvodi: number
    idProizvod: number
    idKorpa: number
    cena: number
    kolicina: number
    urlSlike?: string
    naziv?: string
    idProizvodNavigation?: IdProizvodNavigation
}


export interface Korpa {
    idKorpa: number
    cenaDostave: number
    ukupnaCena: number
    status?: string
    datum?: Date
    paymentIntent?: string
    clientSecret?: string
    idKorisnik?: number
    idDostava: number
    dodatiProizvodi: DodatiProizvodi[]
    idDostavaNavigation?: IdDostavaNavigation
    idPlacanjeNavigation?: IdPlacanjeNavigation
}

export interface Porudzbina {
    cenaDostave: number
    ukupnaCena: number
    idDostava: number
    idKorisnik: number
    dodatiProizvodi?: DodatiProizvodi[]
}

export interface IdDostavaNavigation {
    idDostava: number
    tipDostave: string
    nazivSluzbe: string
    cenaUsluge: number
    rokDostave: string
    korpe: any[]
}

export interface IdPlacanjeNavigation {
    idPlacanje: number
    status: string
    datum: string
    idKorisnik: number
    idKorisnikNavigation: any
    korpe: any[]
}

export interface IdProizvodNavigation {
    idProizvod: number
    naziv: string
    opis: string
    kolicinaNaStanju: number
    cenaPoM2: number
    paketPoM2: number
    dimenzija: string
    nijansa: string
    urlSlike: string
    idKategorija: number
    idProizvodjac: any
    dodatiProizvodi: DodatiProizvodi[]
    idKategorijaNavigation: any
    idProizvodjacNavigation: any
}


export interface IdKorpaNavigation {
    idKorpa: string
    cenaDostave: number
    ukupnaCena: number
    idPlacanje: number
    idDostava: number
    dodatiProizvodi: any[]
    idDostavaNavigation: any
    idPlacanjeNavigation: any
}

export class Korpa implements Korpa {
    idKorpa: number = Math.floor(Math.random() * 100) + 2;
    dodatiProizvodi: DodatiProizvodi[] = [];
}

export interface BasketTotal {
    shipping: number
    subtotal: number
    total: number
}