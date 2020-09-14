import * as React from 'react';
import { observer, inject } from 'mobx-react';

class CountryList extends React.Component {

    componentDidMount() {
        this.props.CountryStore.getCountriesAsync();
    }

    searchCountry = (e) => {
        if (e.key === "Enter") {
            this.props.CountryStore.search = e.target.value;
        }
    }

    sortCountry = () => {
        this.props.CountryStore.countryData.isAscending = this.props.CountryStore.countryData.isAscending 
        ? false : true
        this.props.CountryStore.getCountriesAsync()
    }
    render() {
        return (
            <div>
                <input type="search" placeholder="Search" onKeyPress={this.searchCountry} />
                <input type="submit" value="Sort" onClick={this.sortCountry}/>
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>City</th>
                            <th>Date Created</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.CountryStore.countryData.model.map(country => (
                            <tr key={country.id}>
                                <td>{country.name}</td>
                                <td>{country.city}</td>
                                <td>{country.dateCreated}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        )
    }
}

export default inject("CountryStore")(observer(CountryList));