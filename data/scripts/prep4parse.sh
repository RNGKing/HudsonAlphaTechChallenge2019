#!/bin/bash

count=1
for f in ../raw/*.bed; do
    cut -f1-3 $f | sort -k1,1 -k2,2n \
        | awk -vid=$(basename $f) '{ print $0"\t"id; }' > ../tidy/$count.id.bed;
    let count=$count+1
done
