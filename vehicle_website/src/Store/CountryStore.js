import { observable, runInAction, decorate } from 'mobx';
import CountryService from './CountryService'

class CountryStore {
constructor(){
 this.countryService = new CountryService();
}
 countryData = {
     model: []
 };
 status = "initial";
 searchQuery = "";

 getCountriesAsync = async () => {
     try {
         var params = {
             pageNumber: this.countryData.pageNumber,
             searchQuery: this.searchQuery,
             isAscending: this.countryData.isAscending
         };
         const urlParams = new URLSearchParams(Object.entries(params));
         const data = await this.countryService.get(urlParams)
         runInAction(() => {
             this.countryData = data;
         });
     } catch (error) {
         runInAction(() => {
             this.status = "error";
         });
     }
 };
 createCountryAsync = async (model) => {
     try {
         const response = await this.countryService.post(model);
         if (response.status === 201) {
             runInAction(() => {
                 this.status = "success";
             })
         } 
     } catch (error) {
         runInAction(() => {
             this.status = "error";
         });
     }

 };
 updateCountryAsync = async (vehicle) => {
     try {
         const response = await this.countryService.put(vehicle)
         if (response.status === 200) {
             runInAction(() => {
                 this.status = "success";
             })
         } 
     } catch (error) {
         runInAction(() => {
             this.status = "error";
         });
     }
 };
 deleteCountryAsync = async (id) => {
     try {
         const response = await this.countryService.delete(id);
         if (response.status === 204) {
             runInAction(() => {
                 this.status = "success";
             })
         } 
     } catch (error) {
         runInAction(() => {
             this.status = "error";
         });
     }
 }
}

decorate(CountryStore, {
 countryData: observable,
 searchQuery: observable,
 status: observable
});

export default new CountryStore();