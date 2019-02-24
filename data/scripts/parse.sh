#!/bin/bash

N=${1:-1}
prefix=${2:-../tidy}

# Normalize the prefix path
prefix=$(readlink -f $prefix)

resultfile=$prefix/result.out

# Determine options for overlap
overlap="--bp-ovr $N" # Positive values
if [ $N -le 0 ]; then
    let N=$N*-1
    overlap="--range $N" # Zero or negative (--range 0 is the same as --bp-ovr 1)
fi

echo "Comparing with N=" $N " using " $overlap
echo "Writing result to " $resultfile

# --ec does an error check to confirm we have valid BED data.
# --sweep-all prevents SIGPIPE errors
bedops --everything --ec $prefix/*.in.bed \
    | bedmap --echo $overlap --count --bases --delim '\t' --sweep-all - > $resultfile
