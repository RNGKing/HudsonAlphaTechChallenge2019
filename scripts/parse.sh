#!/bin/bash

N=${1:-1}
prefix=${2:-../results}

# Normalize the prefix path
prefix=$(readlink -f $prefix)

resultfile=$prefix/result.out

# Determine options for overlap
overlap="--bp-ovr $N" # Positive values
if [ $N -le 0 ]; then
    let N=$N*-1
    overlap="--range $N" # Zero or negative (--range 0 is the same as --bp-ovr 1)
fi

echo "Comparing with N =" $N " using " $overlap
echo "Writing result to " $resultfile

# Note that we are not using the --faster option here. That would result in
# better performance, but requires that data has no fully-nested elements.
# See https://bedops.readthedocs.io/en/latest/content/reference/statistics/bedmap.html#using-faster-with-range for details
#
# --ec does an error check to confirm we have valid BED data.
# --sweep-all prevents SIGPIPE errors
# See https://bedops.readthedocs.io/en/latest/content/reference/statistics/bedmap.html#i-o-event-handling for details.
bedops --everything --ec $prefix/*.in.bed \
    | bedmap --echo $overlap --count --bases --delim '\t' --sweep-all - > $resultfile
