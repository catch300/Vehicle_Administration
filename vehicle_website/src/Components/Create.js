class Create extends React.Component {
    CreateCountry = (e) => {
        e.preventDefault();
        this.props.CountryStore.createCountryAsync({
            name: this.refs.name.value,
            city: this.refs.city.value,
        });
        this.refs.name.value = null;
        this.refs.city.value = null;
    };
    render() {
        return (
            <div>
                <div>
                    <form onSubmit={this.CreateCountry}>
                    <div className="form-group">
                        <input ref="name" id="name" type="text" placeholder="Name"/>
                    </div>
                    <div className="form-group">
                        <input ref="city" id="city" type="text" placeholder="City"/>
                    </div>
                        <button type="submit">Save</button>
                    </form>
                </div>
            </div>
        )
    }
}

export default inject("VehicleMakeStore")(observer(Create)); 