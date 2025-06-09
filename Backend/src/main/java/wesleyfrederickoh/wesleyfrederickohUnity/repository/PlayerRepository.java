package wesleyfrederickoh.wesleyfrederickohUnity.repository;

import org.springframework.data.repository.query.Param;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerUpgradeDTO;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import org.springframework.data.jpa.repository.Query;

import java.util.Map;
import java.util.Optional;
import java.util.UUID;
import java.util.List;

@Repository
public interface PlayerRepository extends JpaRepository<Player, UUID> {
    Optional<Player> findByUsername(String username);
    Optional<Player> findByEmail(String email);
    boolean existsByUsername(String username);
    boolean existsByEmail(String email);
    Optional<Player> findByEmailAndPassword(String email, String password);

    @Query(value = "SELECT * FROM player WHERE playerid != :excludePlayerId ORDER BY RANDOM() LIMIT 5", nativeQuery = true)
    List<Player> findRandomPlayers(@Param("excludePlayerId") UUID excludePlayerId);

    @Query(value = "SELECT u.name, u.description, u.base_cost AS baseCost, u.cost_mult AS costMult, pu.level " +
            "FROM player_upgrades pu " +
            "JOIN Player p ON pu.playerid = p.playerid " +
            "JOIN Upgrades u ON pu.upgradeid = u.upgradeid " +
            "WHERE p.username = :username", nativeQuery = true)
    List<Map<String, Object>> findAllUpgradesByUsername(@Param("username") String username);
}
