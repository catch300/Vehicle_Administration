import React from 'react';
import logo from './logo.svg';
import './App.css';
import CountryList from './Components/CountryList'
import { Provider } from 'mobx-react';
import CountryStore from './Store/CountryStore'

function App() {
  return (
    <Provider CountryStore ={CountryStore}>
      <CountryList></CountryList>
    </Provider>
  );
}

export default App;
