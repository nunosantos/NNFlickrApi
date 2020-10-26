import React, { Component } from 'react';
import DataRenderer from './DataRenderer';

export class Form extends Component {
    constructor(props) {
        super(props);
        this.state = { searchString: '', loading: true };
    }


    handleSubmit = async (event) => {
        event.preventDefault();
        const response = await fetch('https://localhost:44318/api/flickr/' + this.state.searchString);
        const data = await response.json();
        this.setState({ flickr: data.photos.photo, loading: false });
    };

    render() {
        let contents = this.state.loading
            ? <p><em>Please input value...</em></p>
            : <DataRenderer data={this.state.flickr} />;

        return (
            <form onSubmit={this.handleSubmit}>
                <input
                    type="text"
                    value={this.state.searchString}
                    onChange={event => this.setState({ searchString: event.target.value })}
                    placeholder="Flickr Category"
                    required
                />
                <button>Search</button>
                {contents}
            </form>

        );
    }
}

export default Form;