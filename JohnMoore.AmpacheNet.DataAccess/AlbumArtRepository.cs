using System;
using System.IO;
using JohnMoore.AmpacheNet.Entities;
using System.Collections.Generic;

namespace JohnMoore.AmpacheNet.DataAccess
{
	public class AlbumArtRepository : IAmpacheSelector<AlbumArt>, IPersistor<AlbumArt>
	{
		private readonly string _directory;
		
		public AlbumArtRepository (string directory)
		{
			if(!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
			_directory = directory;
		}

		#region IAmpacheSelector[JohnMoore.AmpacheNet.Entities.AlbumArt] implementation
		public System.Collections.Generic.IEnumerable<JohnMoore.AmpacheNet.Entities.AlbumArt> SelectAll ()
		{
			throw new NotSupportedException();
		}
	
		public System.Collections.Generic.IEnumerable<JohnMoore.AmpacheNet.Entities.AlbumArt> SelectBy<TParameter> (TParameter parameter) where TParameter : JohnMoore.AmpacheNet.Entities.IEntity
		{
			var art = parameter as IArt;
			if(art == null)
			{
				throw new InvalidOperationException(string.Format("Can not select art with a {0} parameter", typeof(TParameter).Name));
			}
			var res = new List<AlbumArt>();
			try 
			{
				if (File.Exists (Path.Combine (_directory, art.ArtId.ToString ())))
				{
					res.Add (new AlbumArt { ArtistId = art.ArtId, ArtStream = File.OpenRead (Path.Combine (_directory, art.ArtId.ToString ())) });
				} 
				else 
				{
					var con = System.Net.WebRequest.Create (art.ArtUrl);
					var stream = new MemoryStream();
					con.GetResponse ().GetResponseStream ().CopyTo(stream);
					stream.Position = 0;
					res.Add (new AlbumArt { ArtistId = art.ArtId, ArtStream = stream });
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("Exception of type {0} was thrown while getting album art.", ex.GetType().Name));
				Console.WriteLine(ex.Message);
			}
			return res;
		}
	
		public System.Collections.Generic.IEnumerable<JohnMoore.AmpacheNet.Entities.AlbumArt> SelectBy (string searchText)
		{
			throw new NotSupportedException();
		}
	
		public JohnMoore.AmpacheNet.Entities.AlbumArt SelectBy (int ampacheId)
		{
			throw new NotSupportedException();
		}
		#endregion

		#region IPersistor[JohnMoore.AmpacheNet.Entities.AlbumArt] implementation
		public bool IsPersisted (JohnMoore.AmpacheNet.Entities.AlbumArt entity)
		{
			return File.Exists(Path.Combine(_directory, entity.ArtistId.ToString()));
		}
		
		public bool IsPersisted (JohnMoore.AmpacheNet.Entities.IArt entity)
		{
			return File.Exists(Path.Combine(_directory, entity.ArtId.ToString()));
		}
	
		public void Persist (JohnMoore.AmpacheNet.Entities.AlbumArt entity)
		{
			if(IsPersisted(entity))
			{
				Remove(entity);
			}
			var stream = File.Create(Path.Combine(_directory, entity.ArtistId.ToString()));
			entity.ArtStream.Position = 0;
			entity.ArtStream.CopyTo(stream);
			entity.ArtStream.Position = 0;
			stream.Close();
		}
	
		public void Remove (JohnMoore.AmpacheNet.Entities.AlbumArt entity)
		{
			File.Delete(Path.Combine(_directory, entity.ArtistId.ToString()));
		}
		#endregion
	}
}

