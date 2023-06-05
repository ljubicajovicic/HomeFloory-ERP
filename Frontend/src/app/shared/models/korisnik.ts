export interface Korisnik {
    idKorisnik: number
    ime: string
    prezime: string
    datumRodjenja: string
    kontakt: string
    email: string
    lozinka: string
    idAdresaIsporuke: number
    idUloga: 1
    token: string
}

export interface Kor {
    email: string
    lozinka: string
    token: string
}

export interface AdresaIsporuke {
    idAdresaIsporuke?: number
    grad: string | null
    drzava: string | null
    ulica: string | null
    postanskiBroj: string | null
}

export interface IdUlogaNavigation {
    idUloga: number
    uloga1: string
    korisnici: Korisnici | undefined[]
}

export interface Korisnici {
    idKorisnik: number
    ime: string
    prezime: string
    datumRodjenja: string
    kontakt: string
    email: string
    lozinka: string
    idAdresaIsporuke: any
    idUloga: number
    idAdresaIsporukeNavigation: any
    idUlogaNavigation: any
    placanja: any[]
}