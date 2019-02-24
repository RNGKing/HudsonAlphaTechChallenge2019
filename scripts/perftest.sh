#!/bin/bash

timecmd=/usr/bin/time
inputdir=../data/perfdata
outputdir=../results

for f in $(seq $(($1-1))); do
    cp $inputdir/hg38.bed $inputdir/hg38-$f.bed
done

$timecmd ./prep4parse.sh $inputdir $outputdir && $timecmd ./parse.sh 1 $outputdir
