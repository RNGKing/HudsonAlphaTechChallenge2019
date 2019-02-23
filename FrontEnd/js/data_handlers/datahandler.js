const {dialog} = require('electron').remote;
const fs = require('fs');
const EventEmitter = require('events');

class MyEmitter extends EventEmitter {}

const OnDataLoaded = new MyEmitter();



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
            OnDataLoaded.emit('data_loaded', rowData);
            }
        }  
    );
});
