export interface Proizvod {
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
    idProizvodjac?: number
    dodatiProizvodi?: any[]
    idKategorijaNavigation?: IdKategorijaNavigation
    idProizvodjacNavigation?: IdProizvodjacNavigation
}

export interface ProizvodAdd {
    naziv: string
    opis: string
    kolicinaNaStanju: number
    cenaPoM2: number
    paketPoM2: number
    dimenzija: string
    nijansa: string
    urlSlike: string
    idKategorija: number
    idProizvodjac?: number
}


export interface IdKategorijaNavigation {
    idKategorija: number
    nazivKategorije: string
    proizvodi: Proizvod | undefined[]
}

export interface IdProizvodjacNavigation {
    idProizvodjac: number
    nazivProizvodjaca: string
    proizvodi: Proizvod | undefined[]
}
