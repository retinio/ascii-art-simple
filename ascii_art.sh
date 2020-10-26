#!/bin/bash

#clear console
printf "\033c"

enviroments=""
command=""

while getopts f:i:o:t:c: flag
do
    case "${flag}" in
        f) font=${OPTARG};;
        i) input=${OPTARG};;
        o) output=${OPTARG};;
        t) type=${OPTARG};;
        c) convertors=${OPTARG};;
    esac
done

if [ ! -z "${font}" ]; then
   filename="$(basename -- $font)"
   enviroments="--env 'FONT_FILE_PATH=/app/fonts/${filename}' --mount type=bind,source=${font},target=/app/fonts/${filename}"
fi

if [ ! -z "${input}" ]; then
   filename="$(basename -- $input)"
   enviroments=" ${enviroments} --env 'INPUT_FILE_PATH=/app/data/${filename}' --mount type=bind,source=${input},target=/app/data/${filename}"
fi

if [ ! -z "${output}" ]; then
   filename="$(basename -- $output)"
   dir="`dirname "${output}"`"
   mkdir -p "${dir}" && touch "${dir}/${filename}"
   enviroments=" ${enviroments} --env 'OUTPUT_FILE_PATH=/app/data/${filename}' --mount type=bind,source=${output},target=/app/data/${filename}"
fi

if [ ! -z "${type}" ]; then
   enviroments=" ${enviroments} --env 'RUN_TYPE=${type}'"
fi

if [ ! -z "${convertors}" ]; then
   enviroments=" ${enviroments} --env 'CONVERTORS_COUNT=${convertors}'"
fi

docker build -t ascii_art .
command="docker run --rm -it $enviroments ascii_art"
eval $command
