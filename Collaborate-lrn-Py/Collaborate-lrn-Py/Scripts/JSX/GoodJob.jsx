var GoodJob = React.createClass({
    render: function () {
        return (
            <div> Good {this.props.status} </div>
            );
    }
});

React.render(
    <GoodJob status="Job!" ></GoodJob>,
    document.getElementById("result")
    );