const {dialog} = require('electron').remote;
const fs = require('fs');


document.querySelector("#btnLoadFile").addEventListener('click', function(event){
    dialog.showOpenDialog({
        properties:['openFile' ]
    },function(files)
    {
        if(files !== undefined){
            
            let rowData = [];

            let dataFS = fs.readFileSync(files[0]);
                
            let rows = dataFS.toString('utf8').split('\n');
            rows.forEach(function(row){
                let obj = {};
                let col = row.split('\t');
                obj["name"] = col[0];
                obj["startPos"] = col[1];
                obj["endPos"] = col[2];
                obj["intersections"] = col[3];
                obj["pairMatches"] = col[4];
                rowData.push(obj);
            });
            OnDataLoaded(rowData);
            }
        }  
    );
});

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
    document.querySelector("#svgCollection").append(`<h4>Loaded Data ${svgContainers.length}</h2>` +
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

function OnDataLoaded(data){

    console.log("event called");

    document.querySelector("#loadFileContainer").style.visibility = "hidden";
    document.querySelector("#svgContainer").style.visibility = "visible";

    

    AddRowToSvgContainer(data);

}
