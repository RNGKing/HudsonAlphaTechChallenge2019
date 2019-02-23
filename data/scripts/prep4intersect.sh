#!/bin/bash

count=1
for f in ../raw/*.bed; do
    cut -f1-3 $f | sort -k1,1 -k2,2n > ../tidy/$count.bed3
    let count=$count+1
done
