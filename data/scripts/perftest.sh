#!/bin/bash

timecmd=/usr/bin/time
perfdir=../perfdata

# Clean up any previous copies
rm -f $perfdir/hg38-*.bed
rm -f $perfdir/*.in.bed
rm -f $perfdir/result.out

for f in $(seq $(($1-1))); do
    cp $perfdir/hg38.bed $perfdir/hg38-$f.bed
done

$timecmd ./prep4parse.sh $perfdir $perfdir && $timecmd ./parse.sh 1 $perfdir
