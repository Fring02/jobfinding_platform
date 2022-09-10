import { useMemo } from "react";
const range = (start, end) => {
    let length = end - start + 1;
    return Array.from({ length }, (_, idx) => idx + start);
};

export const usePagination = ({
    totalCount, //15
    pageSize = 5,
    siblingCount = 1,
    currentPage
  }) => {
    const paginationRange = useMemo(() => {
      const totalPageCount = Math.ceil(totalCount / pageSize); //5
      if (siblingCount >= totalPageCount) {
        return range(1, totalPageCount);
      }
      const leftSiblingIndex = currentPage - siblingCount; //0 1
      const rightSiblingIndex = currentPage + siblingCount; //2 3
      if(leftSiblingIndex === 0 && rightSiblingIndex === totalPageCount) return range(1, totalPageCount);
      if(leftSiblingIndex === 0 && rightSiblingIndex < totalPageCount) return [currentPage, rightSiblingIndex, rightSiblingIndex + 1];
      if(leftSiblingIndex > 0 && rightSiblingIndex === totalPageCount) return [leftSiblingIndex, currentPage, totalPageCount];
      if(leftSiblingIndex > 0 && rightSiblingIndex < totalPageCount) return [leftSiblingIndex, currentPage, rightSiblingIndex];
      if(currentPage === totalPageCount){
        if(leftSiblingIndex === 1) return [leftSiblingIndex, currentPage];
        else return [leftSiblingIndex - 1, leftSiblingIndex, currentPage];
      }
      return [leftSiblingIndex, currentPage, rightSiblingIndex];
      
    }, [totalCount, pageSize, siblingCount, currentPage]);
  
    return paginationRange;
  };