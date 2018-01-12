using System.Collections.Generic;
using System.IO;

/// <summary>
/// Interface for playback file parsers.
/// </summary>
/// <typeparam name="T">The type of the object to read in.</typeparam>
internal interface IFileParser<T> {

    /// <summary>
    /// Parses the given file, returning the specified type.
    /// </summary>
    /// <param name="f">The file to be parsed.</param>
    /// <returns>An object of the type this parser returns.</returns>
    T ParseFile(FileInfo f);

    /// <summary>
    /// Parses the given file, returning the specified type with an assigned object ID.
    /// </summary>
    /// <param name="f">The file to be parsed.</param>
    /// <param name="id">The ID to assign to the returned object.</param>
    /// <returns>An object of the type this parser returns.</returns>
    T ParseFile(FileInfo f, int id);

    /// <summary>
    /// Parses the given file and inserts instances of the parsed type into the given list.
    /// </summary>
    /// <param name="f">The file to be parsed.</param>
    /// <param name="id">The ID to assign to the returned object.</param>
    /// <param name="list">The list to insert parsed objects into.</param>
    /// <returns>A queue with valid elements from the parsed file.</returns>
    List<T> ParseFileIntoList(FileInfo f, int id, ref List<T> list);
}
