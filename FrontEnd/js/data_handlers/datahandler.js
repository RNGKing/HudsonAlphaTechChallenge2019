const {dialog} = require('electron').remote;
const {readFileSync} = require('fs').readFileSync;

document.querySelector("#btnLoadFile").addEventListener('click', function(event){
    dialog.showOpenDialog({
        properties:['openFile' ]
    },function(files){
        if(files !== undefined){
            console.log("processing the files");


            let rowData = [];

            let data = readFileSync(files[0]);

            let rows = data.split('\n');
            for(let r in rows){
                let obj = {};
                let col = r.split('\t');
                obj.name = col[0];
                obj.start = col[1];
                for(let prop in col[3]){
                    prop.split('|');
                    obj.end = prop[0];
                    obj.intersections = prop[1];
                    obj.pairmatches = prop[2];
                }
                rowData.push(obj);
            }
            console.log(`Number of rows dectected ${rowData.length}`);
        }
    }
    );
});
