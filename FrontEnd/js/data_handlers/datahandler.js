const {dialog} = require('electron').remote;
const fs = require('fs');

function readTextFile(filePath){

    let rawFile = new XMLHttpRequest();
    let data = "";
    rawFile.open("GET", filePath, false);
    rawFile.onreadystatechange = function(){
        if(rawFile.readyState === 4)
        {
            if(rawFile.status === 200 || rawFile.status == 0)
            {
                data = rawFile.responseText;
            }
        }
    }
    rawFile.send(null);
    return data;

}

document.querySelector("#btnLoadFile").addEventListener('click', function(event){
    dialog.showOpenDialog({
        properties:['openFile' ]
    },function(files)
    {
        if(files !== undefined){
            console.log("processing the files");

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
                
            console.log(`${rowData[0].name}`);
            }
        }  
    );
});
