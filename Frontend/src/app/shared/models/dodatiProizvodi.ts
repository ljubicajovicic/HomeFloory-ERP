export interface DodatiProizvodi2 {
    idProizvod: number
    basketId: string
    cena: number
    kolicina: number
    kolicinaPoM2: number
    idKorpaNavigation: IdKorpaNavigation
    idProizvodNavigation: IdProizvodNavigation
}

export interface DodatiProizvodi {
    idProizvod: number
    basketId: string
    cena: number
    kolicina: number
    kolicinaPoM2: number
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

export interface BasketItem {
    idProizvod: number
    naziv: string
    opis: string
    kolicinaNaStanju: number
    cenaPoM2: number
    paketPoM2: number
    dimenzija: string
    nijansa: string
    urlSlike: string

    idKorpaNavigation: IdKorpaNavigation;
    idProizvodNavigation: IdProizvodNavigation;
}

export class Korpa implements Korpa {
    basketId = Math.floor(Math.random() * 100) + 2;
    dodatiProizvodi: DodatiProizvodi[] = [];
}

export interface BasketTotals {
    subtotal: number;
    total: number
}