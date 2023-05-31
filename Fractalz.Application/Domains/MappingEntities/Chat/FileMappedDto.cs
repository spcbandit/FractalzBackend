using System;
using Newtonsoft.Json;

namespace Fractalz.Application.Domains.MappingEntities.Chat;

public class FileMappedDto
{
    /// <summary>
    /// ИД - файла
    /// </summary>
    [JsonProperty("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Path
    /// </summary>
    [JsonProperty("path")]
    public string Path { get; set; }
    
    /// <summary>
    /// Extension
    /// </summary>
    [JsonProperty("extension")]
    public string Extension { get; set; }
    
    /// <summary>
    /// FileName
    /// </summary>
    [JsonProperty("fileName")]
    public string FileName { get; set; }
    
    /// <summary>
    /// ByteLength
    /// </summary>
    [JsonProperty("byteLength")]
    public long ByteLength { get; set; }
}