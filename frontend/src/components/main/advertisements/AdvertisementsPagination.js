import { usePagination } from '../../../hooks/use-pagination';
export default function AdvertisementsPagination(props){

  const { onPageChange, totalCount, siblingCount = 1, currentPage, pageSize } = props;
  const paginationRange = usePagination({ totalCount, pageSize, siblingCount, currentPage });
  // If there are less than 2 times in pagination range we shall not render the component
  if (currentPage === 0 || paginationRange.length < 2) {
    return null;
  }
  const onNext = () => {
    onPageChange(currentPage + 1);
  };
  const onPrevious = () => {
    onPageChange(currentPage - 1);
  };
  let lastPage = paginationRange[paginationRange.length - 1];


    return <nav aria-label="Page navigation example" className="mt-3 d-flex justify-content-center">
              <ul className="pagination justify-content-center">

              {currentPage > 1 && <li className="page-item bg-dark" onClick={onPrevious} aria-label="Previous">
                <button className="page-link bg-dark"><span aria-hidden="true">&laquo;</span></button>
              </li>}

              {paginationRange.map(pageNumber => {
                return (
                  <li onClick={() => onPageChange(pageNumber)} key={pageNumber} 
                  className={'page-item ' + (pageNumber === currentPage ? 'active' : '')}>
                    <span className="page-link bg-dark">{pageNumber}</span>
                  </li>
                );
              })}

              {currentPage < lastPage && <li className="page-item" onClick={onNext}>
                <button className="page-link bg-dark" aria-label="next"><span aria-hidden="true">&raquo;</span></button>
              </li>}
              </ul>
  </nav>;
}