#!/bin/bash

inputdir=${1:-../raw}
outputdir=${2:-../tidy}

count=1
for f in $inputdir/*.bed; do
    cut -f1-3 $f | sort -k1,1 -k2,2n \
        | awk -vid=$(basename $f) '{ print $0"\t"id; }' > $outputdir/$count.in.bed;
    let count=$count+1
done
