using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
	public class Gallery
	{
		public string name;
		public List<Track> tracks = new List<Track>();
		public Gallery(string name)
		{
			this.name = name;
		}
		public void AddTrack(Track track)
		{
			tracks.Add(track);
		}
	}
	public class Signer
	{
		public string name;
		protected string country;
		public List<Album> albums = new List<Album>();
		public Signer(string name)
		{
			this.name = name;
			this.country = "Unknown";
		}
		public Signer(string name, string country)
		{
			this.name = name;
			this.country = country;
		}
		public Album CreateGallery(string name, Genre genre)
		{
			var play = new Album(name, genre, this.name);
			albums.Add(play);
			return play;
		}
	}
	public class Album : Gallery
	{
		public Genre genre;
		public string signer;
		public Album(string name, Genre genre, string signer) : base(name)
		{
			this.genre = genre;
			this.signer = signer;
		}
	}
	public class Track
	{
		public string name;
		protected double duration;
		public Genre genre;
		public string signer;
		public string album;
		public Track(string name, double duration, Genre genre, string signer, string album)
		{
			this.name = name;
			this.duration = duration;
			this.genre = genre;
			this.signer = signer;
			this.album = album;
		}
	}
	public class Playlist : Gallery
	{
		public Playlist(string name) : base(name)
		{
		}
	}
	public class User
	{
		public string name;
		public List<Playlist> playlists = new List<Playlist>();
		public User(string name)
		{
			this.name = name;
		}
		public Playlist CreateGallery(string name)
		{
			var play = new Playlist(name);
			playlists.Add(play);
			return play;
		}
	}
	public enum Genre { Rock, Metallic, Classic, HipHop, Pop, Electronic }
	class Program
	{
		static List<Track> tracks = new List<Track>();
		static List<Album> albums = new List<Album>();
		static List<Signer> signers = new List<Signer>();
		static List<User> users = new List<User>();
		static void Register()
		{
			Console.Write("Сколько пользователей хотите зарегистрировать? - ");
			int kol = Convert.ToInt32(Console.ReadLine());
			for (int i = 0; i < kol; ++i)
			{
				Console.Write("Введите имя пользователя: ");
				users.Add(new User(Console.ReadLine()));
			}
		}
		static void CreatePlaylist(User user)
		{
			Console.Write("Сколько плейлистов? - ");
			int n = Convert.ToInt32(Console.ReadLine());
			for (int j = 0; j < n; ++j)
			{
				Console.Write("Название плейлиста: ");
				string nickname = Console.ReadLine();
				user.CreateGallery(nickname);
			}
		}
		static void AppendTrack(Playlist p)
		{
			Console.Write("Сколько песен? - ");
			int m = Convert.ToInt32(Console.ReadLine());
			for (int k = 0; k < m; ++k)
			{
				Console.Write("Название песни (скопируйте из файла): ");
				string trek = Console.ReadLine();
				foreach (Track t in tracks)
				{
					if (t.name == trek) p.AddTrack(t);
				}
			}
		}
		static void CountAlbums()
		{
			int[] g = new int[6];
			foreach (Album a in albums) { ++g[(int)a.genre]; }
			Console.WriteLine("количество альбомов в каждом жанре: ");
			Console.WriteLine($"Rock - {g[0]}");
			Console.WriteLine($"Metallic - {g[1]}");
			Console.WriteLine($"Classic - {g[2]}");
			Console.WriteLine($"HipHop - {g[3]}");
			Console.WriteLine($"Pop - {g[4]}");
			Console.WriteLine($"Electronic - {g[5]}");
		}
		static void FoundPlays()
		{
			Console.Write("Введите название трека, по которому надо найти плейлисты: ");
			string tr = Console.ReadLine();
			List<Playlist> found_plays = new List<Playlist>();  //сюда заносятся все найденные плейлисты
			foreach (User u in users)
			{
				foreach (Playlist p in u.playlists)
				{
					foreach (Track t in p.tracks)
					{
						if (t.name == tr) found_plays.Add(p);
					}
				}
			}
			foreach (Playlist p in found_plays)
			{
				Console.WriteLine("Все найденные плейлисты: ");
				Console.WriteLine(p.name); //печатаем названия найденных плейлистов
			}
		}
		static void Menu()
		{
			Console.WriteLine("Выберете один из пунктов меню: ");
			Console.WriteLine("0 - зарегистрировать пользователя");
			Console.WriteLine("1 - создать плейлист");
			Console.WriteLine("2 - добавить треки в плейлист");
			Console.WriteLine("3 - найти количество альбомов в каждом жанре");
			Console.WriteLine("4 - найти плейлисты с заданным треком");
			int menu = Convert.ToInt32(Console.ReadLine());
			if (menu == 0) Register();
			else if (menu == 1)
			{
				Console.Write("Назовите пользователя - ");
				string user = Console.ReadLine();
				// ищем пользователя
				foreach (User u in users)
				{
					if (u.name == user) CreatePlaylist(u);
				}
			}
			else if (menu == 2)
			{
				Console.Write("Назовите пользователя - ");
				string username = Console.ReadLine();
				// ищем пользователя
				foreach (User u in users)
				{
					if (u.name == username)
					{
						Console.Write("Назовите плейлист - ");
						string play = Console.ReadLine();
						// ищем плейлист
						foreach (Playlist p in u.playlists)
						{
							if (p.name == play) AppendTrack(p);
						}
					}
				}
			}
			else if (menu == 3) CountAlbums();
			else if (menu == 4) FoundPlays();
			else Console.WriteLine("Неверно указан пункт");
			Console.Write("Продолжить? (1 - да, 0 - нет)");
			if (Convert.ToInt32(Console.ReadLine()) == 1) Menu();
		}
		static Genre FindGenre(int genre)
		{
			switch (genre)
			{
				case 0:
					return Genre.Rock;
				case 1:
					return Genre.Metallic;
				case 2:
					return Genre.Classic;
				case 3:
					return Genre.HipHop;
				case 4:
					return Genre.Pop;
				case 5:
					return Genre.Electronic;
				default:
					return 0;
			}
		}
		static void Main()
		{
			string path = @"input";
			using (StreamReader sr = new StreamReader(path))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string[] info = line.Split(";");
					tracks.Add(new Track(info[0], Convert.ToDouble(info[1]), FindGenre(Convert.ToInt32(info[2])), info[3], info[4]));
				}
			}

			path = @"input1";
			using (StreamReader sr = new StreamReader(path))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string[] info = line.Split(";");
					var a = new Album(info[0], FindGenre(Convert.ToInt32(info[1])), info[2]);
					foreach (Track t in tracks)
					{
						if (t.album == a.name) a.AddTrack(t);
					}
					albums.Add(a);
				}
			}

			path = @"input2";
			using (StreamReader sr = new StreamReader(path))
			{
				string line;
				Signer s = null;
				while ((line = sr.ReadLine()) != null)
				{
					string[] info = line.Split(";");
					if (info.Length == 2)
						s = new Signer(info[0], info[1]);
					else if (info.Length == 1)
						s = new Signer(info[0]);
					else Console.WriteLine("Ошибка: неверный формат информации об исполнителе.");
					foreach (Album a in albums)
					{
						if (a.signer == s.name) s.albums.Add(a);
					}
					signers.Add(s);
				}
			}

			Menu();
		}
	}
}