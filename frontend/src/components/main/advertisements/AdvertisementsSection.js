import React from "react";
import { FilterContextProvider } from "../../../store/filter_context";
import AdvertisementsList from "./AdvertisementsList";
const AdvertisementsSection = () => {

  return <main className="container-fluid">
            <div className="row mt-5 p-4" id="advertisementsSection"> 
              <FilterContextProvider>
                <AdvertisementsList />
              </FilterContextProvider>
            </div>
        </main>;
}
export default AdvertisementsSection;