﻿using Model.DTOs;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Depot
    {
        private readonly ConnectionClass _connect;
        public Depot(ConnectionClass connect)
        {
            _connect = connect;
        }

        public DepotDTO GetDepot(string depotName)
        {
            int trackNumber = 0;
            int sectorNumber = 0;

            DepotDTO depot = new DepotDTO();

            TrackDepotDTO trackDepot = new TrackDepotDTO();

            List<TrackDepotDTO> trackDepots = new List<TrackDepotDTO>();
            List<SectorDepotDTO> sectorDepots = new List<SectorDepotDTO>();
            try
            {
                _connect.Con.Open();

                string query = "SELECT track.TrackNumber, track.Line, sector.Position, sector.idSectorStatus, sector.idTram, remise.Name FROM `track` "
                    + "INNER JOIN sector ON sector.idTrack = track.idTrack "
                    + "INNER JOIN remise ON remise.idRemise = track.idRemise "
                    + "WHERE remise.Name = @name ORDER BY track.TrackNumber, sector.Position";
                MySqlCommand cmd = new MySqlCommand(query, _connect.Con);
                cmd.Parameters.AddWithValue("@Name", depotName);
                var dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        trackDepot = new TrackDepotDTO();
                        SectorDepotDTO sectorDepot = new SectorDepotDTO();
                        depot.Name = dataReader.GetString("Name");
                        trackDepot.Line = dataReader.GetInt32("Line");
                        trackDepot.TrackNumber = dataReader.GetInt32("TrackNumber");
                        sectorDepot.idSectorStatus = dataReader.GetInt32("idSectorStatus");
                        sectorDepot.Position = dataReader.GetInt32("Position");
                        sectorDepot.idTram = dataReader.IsDBNull(dataReader.GetOrdinal("idTram")) ? 0 : dataReader.GetInt32("idTram");
                        if (trackNumber == trackDepot.TrackNumber)
                        {
                            sectorDepots.Add(sectorDepot);
                            trackDepot.Sectors = sectorDepots;
                            trackNumber = trackDepot.TrackNumber;
                        }
                        else
                        {
                            if (trackNumber != 0)
                            {
                                trackDepot.Sectors = sectorDepots;
                                trackDepots.Add(trackDepot);
                                sectorDepots.Clear();
                            }
                            sectorDepots.Add(sectorDepot);
                            trackNumber = trackDepot.TrackNumber;
                        }
                    }
                    trackDepots.Add(trackDepot);
                    depot.Tracks = trackDepots;
                }
                dataReader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _connect.Con.Close();
            }
            return depot;
        }
    }
}
