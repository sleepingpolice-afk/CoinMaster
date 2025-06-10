package wesleyfrederickoh.wesleyfrederickohUnity.controller;

import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerDataResponse;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerLoginRequest;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerPublicDTO;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerUpgradeDTO;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import wesleyfrederickoh.wesleyfrederickohUnity.service.PlayerLoginService;
import wesleyfrederickoh.wesleyfrederickohUnity.service.PlayerService;
import wesleyfrederickoh.wesleyfrederickohUnity.service.PlayerProgressService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Map;
import java.util.UUID;

@RestController
@RequestMapping("/api/players")
public class PlayerController {
    @Autowired
    private PlayerService playerService;

    @Autowired
    private PlayerLoginService playerLoginService;

    @Autowired
    private PlayerProgressService playerProgressService;

    @PostMapping
    public ResponseEntity<Player> createPlayer(@RequestBody Player player) {
        return ResponseEntity.ok(playerService.createPlayer(player));
    }

    @GetMapping("/{username}")
    public ResponseEntity<Player> getPlayer(@PathVariable String username) {
        return playerService.getPlayerByUsername(username)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }

    @PostMapping("/login")
    public ResponseEntity<?> loginPlayer(@RequestBody PlayerLoginRequest loginRequest) {
        try {
            PlayerDataResponse playerData = playerLoginService.loginAndGetPlayerData(loginRequest);
            return ResponseEntity.ok(playerData);
        } catch (RuntimeException e) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(e.getMessage());
        }
    }

    @GetMapping("/{username}/currency")
    public ResponseEntity<Map<String, Long>> getCurrency(@PathVariable String username) {
        return playerService.getCurrencyByUsername(username)
                .map(currency -> ResponseEntity.ok(Map.of("currency", currency)))
                .orElse(ResponseEntity.notFound().build());
    }

    @GetMapping("/random/{excludePlayerId}")
    public ResponseEntity<List<PlayerPublicDTO>> getRandomPlayers(@PathVariable UUID excludePlayerId) {
        List<PlayerPublicDTO> randomPlayers = playerService.getRandomPlayers(excludePlayerId);
        return ResponseEntity.ok(randomPlayers);
    }

    @GetMapping("/{username}/upgrades")
    public ResponseEntity<List<PlayerUpgradeDTO>> getPlayerUpgrades(@PathVariable String username) {
        List<PlayerUpgradeDTO> upgrades = playerService.getUpgradesForPlayer(username);
        return ResponseEntity.ok(upgrades);
    }

    @PostMapping("/{playerId}/decrease-upgrades")
    public ResponseEntity<?> decreasePlayerUpgrades(@PathVariable UUID playerId) {
        try {
            playerProgressService.decreasePlayerUpgradeLevels(playerId);
            return ResponseEntity.ok().body(Map.of("message", "Upgrades decreased successfully for player " + playerId));
        } catch (RuntimeException e) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(Map.of("error", e.getMessage()));
        }
    }

    @PostMapping("/{attackerId}/attack/{defenderId}")
    public ResponseEntity<?> attackPlayer(@PathVariable UUID attackerId, @PathVariable UUID defenderId) {
        boolean success = playerService.decreasePlayerUpgrades(attackerId, defenderId);
        if (success) {
            return ResponseEntity.ok().body(Map.of("message", "Attack successful. Defender's upgrades decreased."));
        }
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(Map.of("error", "Player not found or no upgrades to decrease."));
    }
}
