const fileLoader = require('./datahandler');
const svg = require('./svg');

let svgContainers = []

function FindHighest(data){
    let highest = data[0]["endPos"];
    data.forEach(function(element){
        if(element["startPos"] > highest){
            highest = element["startPos"];
        }
    });
    return highest;
}

function FindLowest(data){
    let lowest = data[0]["startPos"];
    data.forEach(function(element){
        if(element["startPos"] < lowest){
            lowest = element["startPos"];
        }
    });
    return lowest;
}

function AddRowToSvgContainer(data){
    $("#svgCollection").append(`<h4>Loaded Data ${svgContainers.length}</h2>` +
    `<div id="svgElement${svgContainers.length}"></div>`);

    let low = FindLowest(data);
    let high = FindHighest(data);

    let width = high - low;
    let height = 100;

    let draw = svg.SVG(`svgElement${svgContainers.length}`).size(width,height);
    draw.viewbox(0,0,width, height);
    draw.rect(width, height).fill('#dde3e1');

    data.forEach(function(element){
        let boxWidth = element["endPos"] - element["startPos"];
        let rect = draw.rect(boxWidth, 50).fill('#FF0000');
        rect.x(element["startPos"]);
    });
}

fileLoader.OnDataLoaded.on('data_loaded', function(data){
    document.querySelector("#loadFileContainer").style.visibility = "hidden";
    document.querySelector("#svgContainer").style.visibility = "visible";

    

    AddRowToSvgContainer(data);

    
});