

import React, { Component } from 'react'
import { DataProvider } from './Component/Context';
import './style.css';
import Header from './Component/Header';
import Section from './Component/Section';

class App extends Component {
    render() {
        return (
            <DataProvider>
                <div>
                    <Header></Header>
                    <Section></Section>
                </div>
            </DataProvider>
        );
    }

}

export default App;
