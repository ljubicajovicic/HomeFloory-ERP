export interface Pagination<T> {
    data: T,
    pageNumber: number,
    pageSize: number,
    total: number,
    totalPages: number
}